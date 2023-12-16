using TMPro;

public class Employee
{
    public TMP_Text name;
    public TMP_Text cardName;
    public TMP_Text cardDepartment;
    public TMP_Text description;

    public Employee(TMP_Text name, TMP_Text cardName, TMP_Text description)
    {
        this.name = name;
        this.cardName = cardName;
        this.description = description;
    }

    public void SetEmployeeTextData(string name="", string description="")
    {
        this.name.text = name;
        this.cardName.text = name;
        this.description.text = description;
    }
}
