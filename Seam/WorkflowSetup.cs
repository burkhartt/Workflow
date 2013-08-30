using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Seam {
    public class WorkflowSetup<T> where T : WorkflowConfiguration, new() {
        private readonly Queue<Func<T, bool>> steps = new Queue<Func<T, bool>>();

        public WorkflowSetup<T> Do(Func<T, bool> step) {
            steps.Enqueue(step);
            return this;
        }

        public WorkflowSetup<T> Then(Func<T, bool> step) {
            return Do(step);
        }

        public Workflow<T> Compile() {
            var workflowCompiler = new WorkflowCompiler<T>();
            return workflowCompiler.Compile(steps);
        }
    }

    public class WorkflowCompiler<T> where T : IWorkflowConfiguration, new() {
        private readonly Queue<string> stepsMethodNames = new Queue<string>(); 

        public Workflow<T> Compile(Queue<Func<T, bool>> steps) {
            while (steps.Any()) {
                var step = steps.Dequeue();
                var methodName = ExtractMethodName(step);
                
                stepsMethodNames.Enqueue(methodName);
                //GetParameters(step.Method, 0);
            }
            return new Workflow<T>(stepsMethodNames);
        }

        //private Dictionary<string, object> GetParameters(System.Reflection.MethodInfo method, int index) {
        //    var parameters = new Dictionary<string, object>();
        //    parameters[method.GetParameters()[index].Name] = method.
        //    return parameters;
        //}

        private static string ExtractMethodName(Func<T, bool> func)
        {
            var il = func.Method.GetMethodBody().GetILAsByteArray();

            // first byte is ldarg.0
            // second byte is callvirt
            // next four bytes are the MethodDef token
            var mdToken = (il[5] << 24) | (il[4] << 16) | (il[3] << 8) | il[2];
            var innerMethod = func.Method.Module.ResolveMethod(mdToken);

            // Check to see if this is a property getter and grab property if it is...
            if (innerMethod.IsSpecialName && innerMethod.Name.StartsWith("get_"))
            {
                var prop = (from p in innerMethod.DeclaringType.GetProperties()
                            where p.GetGetMethod() == innerMethod
                            select p).FirstOrDefault();
                if (prop != null)
                    return prop.Name;
            }

            return innerMethod.Name;
        }
    }
}