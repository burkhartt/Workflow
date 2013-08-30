using Newtonsoft.Json;

namespace Seam {
    internal class WorkflowRepository<T> where T : IWorkflowConfiguration, new() {
        private string savedWorkflow;

        public void Save(Workflow<T> workflow) {
            savedWorkflow = JsonConvert.SerializeObject(workflow);
        }

        public Workflow<T> Load() {
            return JsonConvert.DeserializeObject<Workflow<T>>(savedWorkflow);
        }
    }
}