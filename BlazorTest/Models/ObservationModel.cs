using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorTest.Models
{
    public class ObservationGroupModel
    {
        public int GrpId { get; set; }
        public string GrpText { get; set; }
    }
    public class ObservationModel
    {
        public int ObsId { get; set; }
        public string ObsText { get; set; }
    }
    public class ObservationViewModel
    {
        public int GrpId { get; set; }
        public string GrpText { get; set; }
        public List<ObservationModel> observations { get; set; }
        public List<ResponseTypeModel> responseTypes { get; set; }
    }
    public class ResponseModel
    {
        public int GrpId { get; set; }
        public int ObsId { get; set; }
        public int Response { get; set; }
        public string Comment { get; set; }
    }
    public class ResponseTypeModel
    {
        public int ResponseId { get; set; }
        public string ResponseText { get; set; }
    }
}
