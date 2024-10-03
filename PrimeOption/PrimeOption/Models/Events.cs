using System;
using Newtonsoft.Json;

namespace PrimeOption.Models
{
	public class Events
	{
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("title")]
        public string title { get; set; }

        [JsonProperty("request")]
        public int request { get; set; }

        [JsonProperty("requestName")]
        public string requestName { get; set; }

        [JsonProperty("flexWorker")]
        public string flexWorker { get; set; }

        [JsonProperty("flexWorkerId")]
        public int flexWorkerId { get; set; }

        [JsonProperty("dateEvent")]
        public DateTime dateEvent { get; set; }

        [JsonProperty("startTimeEvent")]
        public string startTimeEvent { get; set; }

        [JsonProperty("eindTimeEvent")]
        public string eindTimeEvent { get; set; }

        [JsonProperty("pause")]
        public string pause { get; set; }

        [JsonProperty("remark")]
        public string remark { get; set; }

        [JsonProperty("remarkforWorker")]
        public object remarkforWorker { get; set; }

        [JsonProperty("color")]
        public object color { get; set; }

        [JsonProperty("typeOfClient")]
        public object typeOfClient { get; set; }

        [JsonProperty("typeOfhours")]
        public object typeOfhours { get; set; }

        [JsonProperty("totalHours")]
        public double totalHours { get; set; }

        [JsonProperty("quantity")]
        public object quantity { get; set; }

        [JsonProperty("travelAllowancePrice")]
        public object travelAllowancePrice { get; set; }

        [JsonProperty("travelAllowanceType")]
        public object travelAllowanceType { get; set; }

        [JsonProperty("startTimePause")]
        public object startTimePause { get; set; }

        [JsonProperty("eindTimePause")]
        public object eindTimePause { get; set; }

        [JsonProperty("date")]
        public object date { get; set; }

        [JsonProperty("firstName")]
        public string firstName { get; set; }

        [JsonProperty("middleName")]
        public string middleName { get; set; }

        [JsonProperty("lastName")]
        public string lastName { get; set; }

        [JsonProperty("createdBy")]
        public object createdBy { get; set; }

        [JsonProperty("createdOn")]
        public object createdOn { get; set; }

        [JsonProperty("lastEditBy")]
        public object lastEditBy { get; set; }

        [JsonProperty("lastEditOn")]
        public object lastEditOn { get; set; }

        [JsonProperty("weekNumber")]
        public int weekNumber { get; set; }

        [JsonProperty("fullName")]
        public string fullName { get; set; }

        [JsonProperty("companyName")]
        public object companyName { get; set; }

        [JsonProperty("client")]
        public object client { get; set; }

        [JsonProperty("requestAdress")]
        public object requestAdress { get; set; }

        [JsonProperty("payrollNum")]
        public object payrollNum { get; set; }

        [JsonProperty("flexWorkers")]
        public object flexWorkers { get; set; }
    }
}

