using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.StorageClient;

using JobSchedulerWR.WorkerRoleServiceReference;

namespace JobSchedulerWR
{
    public class WorkerRole : RoleEntryPoint
    {
        private WorkerRoleServiceClient wrServiceClient = null;
        private TimeSpan sleep = new TimeSpan(0, 3, 0);  // 3 minuti
        private bool areBudgetComputes = false;

        public override void Run()
        {
            // Implementazione di lavoro di esempio. Sostituire con la logica personalizzata.
            Trace.WriteLine("JobSchedulerWR entry point called", "Information");
            Task task = new Task();
            PushNotificationSender sender = new PushNotificationSender();

            while (true)
            {
                PerformTasks(task);
            }
        }

        public override bool OnStart()
        {
            // Impostare il numero massimo di connessioni simultanee 
            ServicePointManager.DefaultConnectionLimit = 12;

            // Per informazioni sulla gestione delle modifiche alla configurazione,
            // vedere l'argomento MSDN all'indirizzo http://go.microsoft.com/fwlink/?LinkId=166357.

            return base.OnStart();
        }

        private void PerformTasks(Task task)
        {
            // si computano i match alle 15:15 
            if ((DateTime.Now.Hour == task.MatchTime.Hour) && (DateTime.Now.Minute == task.MatchTime.Minute))
            {
                Trace.WriteLine("Start Playing Match...", "Information");

                using (wrServiceClient = new WorkerRoleServiceClient())
                {
                    wrServiceClient.CreateMatch();
                }

                Trace.WriteLine("Match Computed!!", "Information");
                Thread.Sleep(sleep);	// nn fare niente per 3 minuti -> risparmio CPU workload dello schedulatore
            }

            // si computano gli allenamenti
            if ((DateTime.Now.Hour == task.TrainingTime.Hour) && (DateTime.Now.Minute == task.TrainingTime.Minute))
            {
                Trace.WriteLine("Performing Trainings...", "Information");

                using (wrServiceClient = new WorkerRoleServiceClient())
                {
                    wrServiceClient.ComputeTrainings();
                }

                Thread.Sleep(sleep);	// nn fare niente per 3 minuti -> risparmio CPU workload dello schedulatore
                Trace.WriteLine("Trainings Computed!!", "Information");
            }

            // si computano gli acquisti alle 23:15
            if ((DateTime.Now.Hour == task.PurchaseTime.Hour) && (DateTime.Now.Minute == task.PurchaseTime.Minute))
            {
                Trace.WriteLine("Performing Purchases...", "Information");

                using (wrServiceClient = new WorkerRoleServiceClient())
                {
                    wrServiceClient.ComputeDirectlyPurchase();
                }
                
                Thread.Sleep(sleep);	// nn fare niente per 3 minuti -> risparmio CPU workload dello schedulatore
                Trace.WriteLine("Purchases Computed!!", "Information");
            }

            // si computano gli stipendi ogni Sabato alle 1 del mattino!
            if ((DateTime.Today.DayOfWeek == DayOfWeek.Saturday) && (DateTime.Now.Hour == task.BudgetTime.Hour) && (areBudgetComputes == false))
            {
                Trace.WriteLine("Performing Budgets...", "Information");

                using (wrServiceClient = new WorkerRoleServiceClient())
                {
                    wrServiceClient.ComputeAllBudgets();
                    areBudgetComputes = true;
                }
                    
                Thread.Sleep(sleep);	// nn fare niente per 3 minuti -> risparmio CPU workload dello schedulatore
                Trace.WriteLine("Budgets Computed!!", "Information");
            }

            // vogliamo che i budgets siano computati una volta sola al sabato
            if (DateTime.Today.DayOfWeek != DayOfWeek.Saturday)
                areBudgetComputes = false;
        }
    }
}
