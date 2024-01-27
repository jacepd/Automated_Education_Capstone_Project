using AutomatedEducationProgram.Areas.Data;
using AutomatedEducationProgram.Data;
using AutomatedEducationProgram.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AutomatedEducationProgram.Pages.MyNotes
{
    public class EditNoteModel : PageModel
    {
        public Note CurrentNote;
        public IEnumerable<VocabularyWord> Vocab;
        public IEnumerable<ExamQuestion> Questions;
        private readonly AutomatedEducationProgramContext _context;
        private readonly UserManager<AEPUser> _userManager;
        private readonly IConfiguration _configuration;

        public EditNoteModel(AutomatedEducationProgramContext context, UserManager<AEPUser> userManager, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
        }

        public void OnGet(int? noteId)
        {
            CurrentNote = _context.Notes.Where(note => note.Id == noteId).FirstOrDefault();
            Vocab = _context.VocabularyWords.Where(word => word.ParentNote.Id ==  noteId).ToList();
            Questions = _context.ExamQuestions.Where(q => q.ParentNote.Id == noteId).ToList();

        }

        public IActionResult OnPost(int? noteId, IFormCollection inputs)
        {
            List<VocabularyWord> existingVocab = _context.VocabularyWords.Where(word => word.ParentNote.Id == noteId).ToList();
            foreach (var word in existingVocab)
            {
                _context.VocabularyWords.Remove(word);
            }
            List<ExamQuestion> existingQs = _context.ExamQuestions.Where(q => q.ParentNote.Id == noteId).ToList();
            foreach (var q in existingQs)
            {
                _context.ExamQuestions.Remove(q);
            }
            Note toDelete = (_context.Notes.Where(note => note.Id == noteId).ToList())[0];
            _context.Remove(toDelete);

            Note editedNote = new Note();
            editedNote.VocabularyWords = new List<VocabularyWord>();
            editedNote.ExamQuestions = new List<ExamQuestion>();
            foreach (var key in inputs.Keys)
            {
                if (key.StartsWith("title"))
                {
                    editedNote.Title = inputs[key];
                }
                else if (key.StartsWith("description"))
                {
                    editedNote.Description = inputs[key];
                }
                else if (key.StartsWith("userId"))
                {
                    editedNote.UserId = inputs[key];
                }
                else if (key.StartsWith("vocabTerm"))
                {
                    VocabularyWord word = new VocabularyWord();
                    word.ParentNote = editedNote;
                    word.Term = inputs[key];
                    string defKey = key.Replace("Term", "Def");
                    word.Definition = inputs[defKey];
                    editedNote.VocabularyWords.Add(word);
                }
                else if (key.StartsWith("question"))
                {
                    ExamQuestion q = new ExamQuestion();
                    q.ParentNote = editedNote;
                    q.Question = inputs[key];
                    string aKey = key.Replace("question", "ansA");
                    q.AnswerA = inputs[aKey];
                    string bKey = key.Replace("question", "ansB");
                    q.AnswerB = inputs[bKey];
                    string cKey = key.Replace("question", "ansC");
                    q.AnswerC = inputs[cKey];
                    string dKey = key.Replace("question", "ansD");
                    q.AnswerD = inputs[dKey];
                    string eKey = key.Replace("question", "explanation");
                    q.Explanation = inputs[eKey];
                    editedNote.ExamQuestions.Add(q);
                }
            }
            editedNote.CreatedDate = DateTime.Now;
            _context.Notes.Add(editedNote);
            _context.SaveChanges();
            return RedirectToPage("MyNotes");
        }
    }
}