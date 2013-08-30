using System;
using System.Collections.Generic;
using System.Linq;

namespace Seam {
    public class Workflow<T> where T : IWorkflowConfiguration, new() {
        public Queue<string> Steps { get; set; }
        public List<string> CompletedSteps = new List<string>();
        
        public Workflow() {}

        public Workflow(Queue<string> steps) {
            Id = Guid.NewGuid();
            Steps = steps;
            CreateDate = DateTime.Now;
            NextStep = steps.Dequeue();
        }

        public bool IsComplete {
            get { return !Steps.Any(); }
        }

        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string NextStep { get; set; }
        
        public void Process() {
            var workflowConfiguration = new T();
            do {
                var method = workflowConfiguration.GetType().GetMethod(NextStep);
                var wasSuccessful = (bool)method.Invoke(workflowConfiguration, null);
                if (!wasSuccessful) {
                    return;
                }
                CompletedSteps.Add(NextStep);
                NextStep = Steps.Dequeue();
            } while (Steps.Any());                
            
        }
    }
}