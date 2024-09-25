using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG.Managers
{
    public class JobManager
    {
        private static JobManager? _instance = null;
        public static JobManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new JobManager();

                return _instance;
            }
        }

        public bool IsExit { get; private set; }

        JobQueue _jobQueue = new JobQueue();

        public void Push(Action job)
        {
            _jobQueue.Push(job);
        }

        public void Flush()
        {
            _jobQueue.Flush();
        }

        public void Exit()
        {
            IsExit = true;
        }
    }
    #region JobQueue
    interface IJobQueue
    {
        void Push(Action job);
    }
    class JobQueue : IJobQueue
    {
        Queue<Action> _jobQueue = new Queue<Action>();
        object _lock = new object();
        bool _flush = false;

        public void Push(Action job)
        {
            bool flush = false;
            lock (_lock)
            {
                _jobQueue.Enqueue(job);
                if (_flush == false)
                    flush = _flush = true;
            }

            if (flush)
                Flush();
        }

        public void Flush()
        {
            while (true)
            {
                Action? action = Pop();
                if (action == null) return;

                action.Invoke();
            }
        }


        Action? Pop()
        {
            lock (_lock)
            {
                if (_jobQueue.Count == 0)
                {
                    _flush = false;
                    return null;
                }
                return _jobQueue.Dequeue();
            }
        }
    }
    #endregion

}
