namespace Domain.Ticket
{
    public class Activity
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public int Effort { get; private set; }

        public Activity(string title, string description, int effort)
        {
            Title = title;
            Description = description;
            Effort = effort;
        }
    }
}
