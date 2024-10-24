public class MathAssignment : Assignment
{
    private string _homeworklist;
    
    public MathAssignment (string studentName, string topic, string homeworklist)
        :base(studentName, topic)
    {
        _homeworklist = homeworklist;
    }
    public string GetHomeworkList()
    {
        return _homeworklist;
    }
}