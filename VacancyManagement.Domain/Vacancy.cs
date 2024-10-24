﻿namespace VacancyManagement.Domain
{
    public class Vacancy
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public List<Question> Questions { get; set; }

    }
}
