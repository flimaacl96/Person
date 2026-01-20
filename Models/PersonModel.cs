namespace Person.Models;

public class PersonModel
{
    public PersonModel(string name, string cpf)
    {
        Name = name;
        Id = Guid.NewGuid();
        Cpf = cpf;
    }
    public Guid Id {get; init;}
    public string? Name { get; private set; }
    public string? Cpf { get; private set; }
    public void ChangeName(string name)
    {
        Name = name;
    }
    public void Deactivate()
       
    {
        Name = "Desativado";
    }

    public ICollection<EmployeeModel>? Employees { get; set; } = new List<EmployeeModel>();
}