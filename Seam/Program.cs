namespace Seam {
    internal class Program {
        private static void Main() {
            var workflowRepository = new WorkflowRepository<Configuration>();

            var workflowSetup = new WorkflowSetup<Configuration>();
            var workflow = workflowSetup.Do(x => x.SendOrderToWarehouse())
                                        .Then(x => x.WaitForOrderToBeShipped())
                                        .Then(x => x.SendShippingEmail())
                                        .Then(x => x.MarkOrderAsFulfilled())
                                        .Compile();
            workflowRepository.Save(workflow);
            var loadedWorkflow = workflowRepository.Load();
            loadedWorkflow.Process();
        }
    }
}