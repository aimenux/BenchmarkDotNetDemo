using AutoMapper;
using BenchmarkDotNet.Attributes;

namespace Example05;

[Config(typeof(BenchmarkConfig))]
public class BenchmarkSample
{
    private IMapper _mapper;

    private readonly Person _person = new Person
    {
        Id = 1,
        FullName = "Walter White",
        Email = "walter@white.com",
        BirthDate = DateTime.Now.AddYears(-20)
    };

    [GlobalSetup]
    public void Setup()
    {
        var configuration = new MapperConfiguration(cfg => 
        {
            cfg.CreateMap<Person, PersonDto>();
        });
        _mapper = configuration.CreateMapper();
    }

    [Benchmark]
    public PersonDto MapToDto() => _mapper.Map<PersonDto>(_person);

    public class Person
    {
        public int Id { get; init; }
        public string FullName { get; init; }
        public string Email { get; init; }
        public DateTime BirthDate { get; init; }
    }
    
    public class PersonDto
    {
        public int Id { get; init; }
        public string FullName { get; init; }
        public string Email { get; init; }
        public DateTime BirthDate { get; init; }
    }
}