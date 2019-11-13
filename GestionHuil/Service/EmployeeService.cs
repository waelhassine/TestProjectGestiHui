using GestionHuil.Data;
using GestionHuil.Models;
using System;
using System.Collections.Generic;
using System.Linq;

public interface IEmployeeService
{
    Employee Authenticate(string email, string password);
    IEnumerable<Employee> GetAll();
    Employee GetById(int id);
    Employee Create(Employee employee, string password);
    void Update(Employee employee, string password = null);
    void Delete(int id);
}
public class EmployeeService : IEmployeeService
{
    private DataContext _context;

    public EmployeeService(DataContext context)
    {
        _context = context;
    }

    public Employee Authenticate(string email, string password)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            return null;

        var employee = _context.Employees.SingleOrDefault(x => x.Email == email);

        // check if username exists
        if (employee == null)
            return null;

        // check if password is correct
        if (!VerifyPasswordHash(password, employee.PasswordHash, employee.PasswordSalt))
            return null;

        // authentication successful
        return employee;
    }

    public IEnumerable<Employee> GetAll()
    {
        return _context.Employees;
    }

    public Employee GetById(int id)
    {
        return _context.Employees.Find(id);
    }

    public Employee Create(Employee employee, string password)
    {
        // validation
        //if (string.IsNullOrWhiteSpace(password))
        //   throw new AppException("Password is required");

        // if (_context.Employees.Any(x => x.Email == employee.Email))
        //  return NotFound();

        byte[] passwordHash, passwordSalt;
        CreatePasswordHash(password, out passwordHash, out passwordSalt);

        employee.PasswordHash = passwordHash;
        employee.PasswordSalt = passwordSalt;

        _context.Employees.Add(employee);
        _context.SaveChanges();

        return employee;
    }

    public void Update(Employee employeeParam, string password = null)
    {
        var employee = _context.Employees.Find(employeeParam.Id);

        if (employee == null)
            throw new Exception("User not found");

        if (employeeParam.Email != employee.Email)
        {
            // username has changed so check if the new username is already taken
            if (_context.Employees.Any(x => x.Email == employeeParam.Email))
                throw new Exception("Email " + employeeParam.Email + " is already taken");
        }

        // update user properties
        employee.NomPrenom = employeeParam.NomPrenom;
        employee.Email = employeeParam.Email;
        employee.Tel = employeeParam.Tel;
        employee.Photo = employeeParam.Photo;
        employee.Fonction = employeeParam.Fonction;
        employee.Status = employeeParam.Status;

        // update password if it was entered
        if (!string.IsNullOrWhiteSpace(password))
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            employee.PasswordHash = passwordHash;
            employee.PasswordSalt = passwordSalt;
        }

        _context.Employees.Update(employee);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var employee = _context.Employees.Find(id);
        if (employee != null)
        {
            _context.Employees.Remove(employee);
            _context.SaveChanges();
        }
    }

    // private helper methods

    private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        if (password == null) throw new ArgumentNullException("password");
        if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

        using (var hmac = new System.Security.Cryptography.HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
    {
        if (password == null) throw new ArgumentNullException("password");
        if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
        if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
        if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

        using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
        {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != storedHash[i]) return false;
            }
        }

        return true;
    }
}


