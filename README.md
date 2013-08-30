Workflow
========
<p>Example usage:</p>
````
var workflowSetup = new WorkflowSetup<Configuration>();<br />
var workflow = workflowSetup.Do(x => x.SendOrderToWarehouse())<br />
                            .Then(x => x.WaitForOrderToBeShipped())<br />
                            .Then(x => x.SendShippingEmail())<br />
                            .Then(x => x.MarkOrderAsFulfilled())<br />
                            .Compile();<br />
workflow.Process();
````
