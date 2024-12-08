using server.Models;
using server.Repositories.Interfaces;
using server.Services.Interfaces;

namespace server.Services
{
    public class AuctionResultService : IAuctionResultService
    {
        private readonly IAuctionResultRepository _auctionResultRepository;
        //private readonly IAuctionService _auctionService;

        public AuctionResultService(IAuctionResultRepository auctionResultRepository)
        {
            _auctionResultRepository = auctionResultRepository;
            //_auctionService = auctionService;
        }

        public async Task<IEnumerable<AuctionResult>> GetAllAuctionResultsAsync()
        {
            return await _auctionResultRepository.GetAllAuctionResults();
        }

        public async Task<AuctionResult> GetAuctionResultByIdAsync(int resultId)
        {
            var result = await _auctionResultRepository.GetAuctionResultById(resultId);

            
            if (result == null)
            {
                throw new KeyNotFoundException("Auction result not found.");
            }

            return result;
        }

        public async Task<IEnumerable<AuctionResult>> GetAuctionResultsByAuctionIdAsync(int auctionId)
        {
            var results = await _auctionResultRepository.GetAuctionResultsByAuctionId(auctionId);

            if (results == null)
            {
                throw new Exception($"Auction results not found with auction id {auctionId}.");
            }

            return results;
        }

        public async Task AddAuctionResultAsync(AuctionResult auctionResult)
        {
            
            

            if (auctionResult.Status != "Completed")
            {
                throw new InvalidOperationException("Results can only be added for auctions that have been completed.");
            }

            await _auctionResultRepository.AddAuctionResult(auctionResult);
        }

        public async Task UpdateAuctionResultAsync(AuctionResult auctionResult)
        {
            // Validation: Check if the result exists
            var existingResult = await _auctionResultRepository.GetAuctionResultById(auctionResult.ResultId);
            if (existingResult == null)
            {
                throw new KeyNotFoundException("Auction result not found.");
            }

            // Validation: Ensure updates are allowed for completed auctions
            if (existingResult.Status != "Pending")
            {
                throw new InvalidOperationException("Only pending auction results can be updated.");
            }
            

            

            await _auctionResultRepository.UpdateAuctionResult(auctionResult);
        }

        public async Task DeleteAuctionResultAsync(int resultId)
        {
            // Validation: Check if the result exists
            var existingResult = await _auctionResultRepository.GetAuctionResultById(resultId);
            if (existingResult == null)
            {
                throw new KeyNotFoundException("Auction result not found.");
            }

            // Validation: Ensure deletion is allowed for specific auction statuses
            await _auctionResultRepository.DeleteAuctionResult(resultId);
        }

        public async Task<IEnumerable<AuctionResult>> GetAuctionResultByStatus(string status)
        {   
            
            if(string.IsNullOrEmpty(status))
                throw new Exception("Status can't be empty");
            var results = await _auctionResultRepository.AuctionResultByStatus(status);

            if(results == null)
                throw new Exception($"No auction results with status {status}");

            return results;

        }
    }
}
