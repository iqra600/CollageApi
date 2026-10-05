using CollageApi.Data;
using System.Net;

namespace CollageApi.CommonResponse
{
    public class CommonResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool Status { get; set; }
        //public dynamic MyProperty { get; set; }
        public List<T> ModelDataList { get; set; }
        public T ModelData { get; set; }
       // public dynamic data { get; set; }
        public List<string> Errors { get; set; }

    }
   
}
