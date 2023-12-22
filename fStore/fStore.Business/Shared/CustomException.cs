namespace fStore.Business;

public class CustomException : Exception
{
    public int StatusCode { get; set; }

    public CustomException(int statusCode, string msg) : base(msg)
    {
        StatusCode = statusCode;
    }

    public static CustomException NotFoundException(string msg = "Not found")
    {
        return new CustomException(404, msg);
    }

    public static CustomException WrongCredentialsException(string msg = "Wrong User's credentials")
    {
        return new CustomException(401, msg);
    }

    public static CustomException EmailAvailable(string msg = "This Eamil is exist")
    {
        return new CustomException(400, msg);
    }

    public static CustomException TokenNotCreated(string msg = "Unable to create token")
    {
        return new CustomException(500, msg);
    }

    public static CustomException UnableToMap(string msg = "Unable to map")
    {
        return new CustomException(500, msg);
    }
}
