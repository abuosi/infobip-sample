namespace api.Models;

public class Configuration
{
    public IConfiguration _config { get; set; }    

    public String INFOBIP_URL {get;}
    public String INFOBIP_APIKEY {get;}

    public Configuration (IConfiguration config) {
        _config = config;
        INFOBIP_URL = _config["INFOBIP_URL"]!;
        INFOBIP_APIKEY = _config["INFOBIP_APIKEY"]!;
    }


}