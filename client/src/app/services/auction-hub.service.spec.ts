import { TestBed } from '@angular/core/testing';

import { AuctionHubService } from './auction-hub.service';

describe('AuctionHubService', () => {
  let service: AuctionHubService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AuctionHubService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});


