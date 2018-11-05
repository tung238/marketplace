import { ListingAddModule } from './listing-add.module';

describe('ListingAddModule', () => {
  let listingAddModule: ListingAddModule;

  beforeEach(() => {
    listingAddModule = new ListingAddModule();
  });

  it('should create an instance', () => {
    expect(listingAddModule).toBeTruthy();
  });
});
