import { MetaFieldModule } from './meta-field.module';

describe('MetaFieldModule', () => {
  let metaFieldModule: MetaFieldModule;

  beforeEach(() => {
    metaFieldModule = new MetaFieldModule();
  });

  it('should create an instance', () => {
    expect(metaFieldModule).toBeTruthy();
  });
});
