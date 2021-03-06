import {TestBed} from '@angular/core/testing';

import {ProductService} from './product.service';
import {HttpClientModule} from '@angular/common/http';

describe('ProductService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      HttpClientModule
    ],
    providers: [
      {provide: 'BASE_URL', useValue: 'http://localhost'}
    ]
  }));

  it('should be created', () => {
    const service: ProductService = TestBed.get(ProductService);
    expect(service).toBeTruthy();
  });
});
