using FormBuilder.Data;
using FormBuilder.Enums;
using Microsoft.AspNetCore.Mvc;

[Route("api/forms")]
[ApiController]
public class OdooController : ControllerBase
{
    private readonly AppDbContext _db;

    public OdooController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet("aggregated")]
    public IActionResult GetAggregatedResults([FromQuery] string token)
    {
        var user = _db.Users.FirstOrDefault(u => u.ApiToken == token);
        if (user == null)
            return Unauthorized();

        var formTemplates = _db.FormTemplates
      .Where(t => t.UserId == user.Id)
      .Select(t => new
      {
          t.Title,
          Questions = t.Questions.Select(q => new
          {
              q.Text,
              q.Type,
              Answers = q.Template.Answers.Select(a => a.Response).ToList()
          }).ToList()
      })
      .ToList();

        var results = formTemplates.Select(t => new
        {
            Title = t.Title.ToString(),
            Questions = t.Questions.Select(q => {
                IEnumerable<double> numericValues = Enumerable.Empty<double>();
                if (q.Type == QuestionType.Number)
                {
                    numericValues = q.Answers
                        .Select(r => double.TryParse(r, out var val) ? (double?)val : null)
                        .Where(v => v.HasValue)
                        .Select(v => v.Value);
                }

                return new
                {
                    q.Text,
                    Type = q.Type.ToString(),
                    AnswerCount = q.Answers.Count,
                    Average = numericValues.Any() ? numericValues.Average() : (double?)null,
                    Min = numericValues.Any() ? numericValues.Min() : (double?)null,
                    Max = numericValues.Any() ? numericValues.Max() : (double?)null,
                    MostCommonAnswers = q.Type == QuestionType.ShortText || q.Type == QuestionType.LongText
                        ? q.Answers
                            .GroupBy(a => a)
                            .OrderByDescending(g => g.Count())
                            .Take(3)
                            .Select(g => new { Answer = g.Key, Count = g.Count() })
                            .ToList()
                        : null
                };
            })
        }).ToList();


        return Ok(results);
    }
}
