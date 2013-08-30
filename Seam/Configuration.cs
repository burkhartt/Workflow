namespace Seam {
    public class Configuration : WorkflowConfiguration {
        public bool SendOrderToWarehouse() {
            return true;
        }

        public bool WaitForOrderToBeShipped() {
            return true;
        }

        public bool SendShippingEmail() {
            return true;
        }

        public bool MarkOrderAsFulfilled() {
            return true;
        }
    }
}