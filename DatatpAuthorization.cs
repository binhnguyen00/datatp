using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace datatp {
  public class DatatpAuthorization {
    /*
    public class LoginModel {
      private String     loginId;
      private String     authorization;
      private String     company;
      private String     password;
      private int        timeToLiveInMin;
      private AccessType accessType = AccessType.Employee;
    }
    */

    public string CreateLoginModel(string loginId, string password, string company) {
      Dictionary<string, object> loginModelMap = new Dictionary<string, object> {
        { "loginId", loginId },
        { "password", password },
        { "company", company },
        { "accessType", "Employee" },
        { "validFor", 3 }
      };
      string loginModelJson = JsonConvert.SerializeObject(loginModelMap);
      return loginModelJson;
    }
  }
}
