import { ListingsModule } from './listings.module';

describe('ListingsModule', () => {
  let listingsModule: ListingsModule;

  beforeEach(() => {
    listingsModule = new ListingsModule();
  });

  it('should create an instance', () => {
    expect(listingsModule).toBeTruthy();
  });
});
