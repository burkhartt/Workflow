Workflow
========
<p>Example usage:</p>
````
var workflowSetup = new WorkflowSetup<Configuration>();
var workflow = workflowSetup.Do(x => x.SendOrderToWarehouse())
                            .Then(x => x.WaitForOrderToBeShipped())
                            .Then(x => x.SendShippingEmail())
                            .Then(x => x.MarkOrderAsFulfilled())
                            .Compile();
workflow.Process();
````
