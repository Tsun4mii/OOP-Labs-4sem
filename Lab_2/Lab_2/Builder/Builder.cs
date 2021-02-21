using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2.Builder
{
    public abstract class WorkerBuilder
    {
        public Worker worker { get; private set; }
        public void CreateWorker()
        {
            worker = new Worker();
        }
        public abstract void setFIO(string FIO);
        public abstract void setAge(int age);
        public abstract void setWorkExpirience(int workExpirience);
        public abstract void setPost(string post);
    }
    public class WorkerCreator
    {
        public Worker Create(WorkerBuilder builder, string FIO, int age, int workExp)
        {
            builder.CreateWorker();
            builder.setFIO(FIO);
            builder.setAge(age);
            builder.setWorkExpirience(workExp);
            return builder.worker;
        }
    }
    public class BuilderW : WorkerBuilder
    {
        public override void setFIO(string FIO)
        {
            worker.FIO = FIO;
        }
        public override void setAge(int age)
        {
            worker.age = age;
        }
        public override void setWorkExpirience(int workExpirience)
        {
            worker.workExpirience = workExpirience;
        }
        public override void setPost(string post)
        {
            worker.post = post; 
        }
    }
}
