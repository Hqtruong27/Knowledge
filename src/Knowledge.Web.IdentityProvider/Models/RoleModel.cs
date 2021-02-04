namespace Knowledge.Web.IdentityProvider.Models
{
    /// <summary>
    /// RoleModel Request
    /// </summary>
    public class RoleRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
    /// <summary>
    /// RoleModel Response
    /// </summary>
    public class RoleResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
