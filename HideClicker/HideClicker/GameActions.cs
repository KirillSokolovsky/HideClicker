namespace HideClicker
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class GameAction
    {
        public GameCore Core { get; set; }
        public GroupAction Parent { get; set; }
        public string Name { get; }
        protected Action _action;
        public GameAction(string name, Action action)
        {
            Name = name;
            _action = action;
        }

        public abstract void Execute();
    }

    public class SimpleAction : GameAction
    {
        public SimpleAction(string name, Action action) 
            : base(name, action)
        {
        }

        public override void Execute()
        {
            _action();
        }
    }

    public class PeriodicalAction : GameAction
    {
        protected TimeSpan _lastFor;
        protected TimeSpan _sleepFor;

        private Stopwatch _sw;

        public PeriodicalAction(string name, Action action, TimeSpan lastFor, TimeSpan sleepFor)
            : base(name, action)
        {
            _lastFor = lastFor;
            _sleepFor = sleepFor;
        }

        public override void Execute()
        {
            _sw = Stopwatch.StartNew();
            while (_sw.Elapsed < _lastFor)
            {
                if (Core.CTS.IsCancellationRequested) 
                {
                    _sw.Stop();
                    return;
                }
                _action();
                Thread.Sleep(_sleepFor);
            }
        }
    }

    public class RepeatAction : GameAction
    {
        protected TimeSpan _sleepFor;
        protected int _repeatCount;

        public RepeatAction(string name, Action action, int count, TimeSpan sleepFor)
            : base(name, action)
        {
            _repeatCount = count;
            _sleepFor = sleepFor;
        }

        public override void Execute()
        {
            for(int i = 0; i < _repeatCount; i++)
            {
                if (Core.CTS.IsCancellationRequested) return;
                _action();
                Thread.Sleep(_sleepFor);
            }
        }
    }

    public class GroupAction : GameAction
    {
        private List<GameAction> _actions = new List<GameAction>();

        public GroupAction(string name, params GameAction[] actions) 
            : base(name, null)
        {
            _actions.AddRange(actions);
        }

        public void _Add(GameAction action)
        {
            _actions.Add(action);
        }

        public override void Execute()
        {
            foreach (var action in _actions)
            {
                if (Core.CTS.IsCancellationRequested)
                {
                    return;
                }
                action.Execute();
            }
        }
    }

    public class GameCore
    {
        public static GameCore GetNew(string name)
        {
            var gc = new GameCore();
            var g = new GroupAction(name);
            g.Core = gc;
            gc.RootGroupAction = g;
            return gc;
        }

        public GroupAction RootGroupAction { get; set; }
        public CancellationTokenSource CTS { get; internal set; }

        public void Execute()
        {
            RootGroupAction.Execute();
        }
    }

    public static class Ext
    {
        public static GroupAction AddSimple(this GroupAction ga, string name, Action action)
        {
            ga._Add(new SimpleAction(name, action) { Parent = ga, Core = ga.Core });
            return ga;
        }
        public static GroupAction AddPeriodical(this GroupAction ga, string name, Action action, TimeSpan lastFor, TimeSpan sleepFor)
        {
            ga._Add(new PeriodicalAction(name, action, lastFor, sleepFor) { Parent = ga, Core = ga.Core });
            return ga;
        }
        public static GroupAction AddRepeat(this GroupAction ga, string name, Action action, int count, TimeSpan sleepFor)
        {
            ga._Add(new RepeatAction(name, action, count, sleepFor) { Parent = ga, Core = ga.Core });
            return ga;
        }
        public static GroupAction AddGroup(this GroupAction ga, string name)
        {
            var g = new GroupAction(name) { Parent = ga, Core = ga.Core };
            ga._Add(g);
            return g;
        }
        public static GroupAction End(this GroupAction ga)
        {
            return ga.Parent;
        }
        public static GroupAction AddWait(this GroupAction ga, int seconds)
        {
            return ga.AddSimple("Wait", () => Thread.Sleep(TimeSpan.FromSeconds(seconds)));
        }
    }
}
