namespace heimdall.Enums;
/// <summary>
/// Authentication method type
/// </summary>
public enum AuthenticationMethod
{
    /// <summary>
    /// None auth
    /// </summary>
    None = 0,
    /// <summary>
    /// Username and password auth
    /// </summary>
    Password = 1,
    /// <summary>
    /// SSH private key auth
    /// </summary>
    PrivateKey = 2
}
