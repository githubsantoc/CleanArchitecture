
using Domains.Entities;
using FireSharp;
using FireSharp.Config;
using FireSharp.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SentimentMLModel_App;


namespace CQRSApplication.Pages
{
    public class ReviewModel : PageModel
    {
        private readonly FirebaseConfig config;
        private readonly FirebaseClient firebaseClient;
        
        public ReviewModel()
        {
            config = new FirebaseConfig
            {
                BasePath = "https://reviewdatabase-16789-default-rtdb.asia-southeast1.firebasedatabase.app",
                AuthSecret = "Pas9IxuIBno3Vz7y1rio9DHBZxyBxfoZCADYpjBr"
            };
            firebaseClient = new FirebaseClient(config);
            
        }

        [BindProperty]
        public List<string> Reviews { get; set; }
        public string Predict { get; set; }
        public int positiveCount { get; set; } = 0;
        public int negativeCount { get; set; } = 0;
        public int neutralCount { get; set; } = 0;

        public async Task<IActionResult> OnGetAsync()
        {
            var reviewsResponse = await firebaseClient.GetAsync("Review");
            var reviews = JsonConvert.DeserializeObject<Dictionary<string, string>>(reviewsResponse.Body);
            Reviews = reviews!.Values.ToList();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string review)
        {
            await firebaseClient.PushAsync("Review", review);

            return RedirectToPage("/Review");
        }

        public async Task UpdateFirebase(string key, int value)
        {
            SetResponse response = await firebaseClient.SetAsync("SentimentCounts/" + key, value);
        }
        public string GetColor(string predict)
        {
            if (predict == "positive")
            {
                positiveCount++;
                 UpdateFirebase("positiveCount", positiveCount);
                return "green";
            }
            else if (predict == "negative")
            {
                negativeCount++;
                UpdateFirebase("negativeCount", negativeCount);
                return "red";
            }
            else
                neutralCount++;
            UpdateFirebase("neutralCount", neutralCount);
            return "grey";
        }
        public string GetPrediction(string review)
        {
            var sampleData = new SentimentMLModel.ModelInput()
            {
                Text = review
            };

            // Load model and predict output of sample data
            var result = SentimentMLModel.Predict(sampleData);

            return result.PredictedLabel;
        }
    }
}
