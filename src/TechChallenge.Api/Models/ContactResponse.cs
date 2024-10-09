using Microsoft.Extensions.ObjectPool;

namespace TechChallenge.Api.Models;

public class ContactResponse<T>
{

    public bool Success { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
}