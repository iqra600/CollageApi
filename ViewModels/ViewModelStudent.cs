namespace CollageApi.ViewModels
{
    public class ViewModelStudent
    {


      
           
            public int ID { get; set; }
            public string NameDto { get; set; }
            public string Email { get; set; }
            public int Age { get; set; }
        public bool isEligible { get; set; }

        ////public string Password { get; set; }
        ////public string confirmPassword { get; set; }
        public DateTime Date { get; set; } = DateTime.Now.Date;
        }

    
}
