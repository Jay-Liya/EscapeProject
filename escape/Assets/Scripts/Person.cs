using SQLite4Unity3d;

//Class to hold person details, Id, Name, Surname and Age
public class Person  {

	[PrimaryKey, AutoIncrement]
	public int Id { get; set; }
	public string Name { get; set; }
    public string Password { get; set; }
    public string Surname { get; set; }
	public int Age { get; set; }

	public override string ToString ()
	{
		return string.Format ("[Person: Id={0}, Name={1}, Password={2},  Surname={3}, Age={4}]", Id, Name, Password, Surname, Age);
	}
}
