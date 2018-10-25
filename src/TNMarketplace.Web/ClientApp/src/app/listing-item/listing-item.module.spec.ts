import { ListingItemModule } from './listing-item.module';

describe('ListingItemModule', () => {
  let listingItemModule: ListingItemModule;

  beforeEach(() => {
    listingItemModule = new ListingItemModule();
  });

  it('should create an instance', () => {
    expect(listingItemModule).toBeTruthy();
  });
});
