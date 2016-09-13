using System;
using System.Collections.Generic;
using System.Linq;
using ConfApp.Domain;
using ConfApp.Domain.Exceptions;
using ConfApp.Domain.Data;
using ConfApp.Domain.Models;

namespace ConfApp.Data.Repositories
{
    public class ConferenceRepository : IConferenceRepository
    {
        private readonly IReadContext _context;

        public ConferenceRepository(IReadContext context)
        {
            _context = context;
        }

        public List<ConferenceSummary> FindAll(int top, int skip)
        {
            return _context
                .ConferenceSummaries
                .OrderBy(x => x.Name)
                .Skip(skip)
                .Take(top)
                .ToList();
        }

        public ConferenceDetails FindById(Guid id)
        {
            try
            {
                return _context
                .Conferences
                .First(x => x.Id == id);
            }
            catch (InvalidOperationException ex)
            {
                throw new EntityNotFoundException(nameof(Conference), id.ToString(), ex);
            }
        }

        public Conference Save(Conference conference)
        {
            conference.Id = Guid.NewGuid();
            //_context.Conferences.Add(conference);
            _context.SaveChangesAsync().Wait();

            return conference;
        }
    }
}