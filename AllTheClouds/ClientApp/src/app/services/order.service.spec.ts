import {TestBed} from '@angular/core/testing';

import {OrderService} from './order.service';
import {HttpClientModule} from '@angular/common/http';

describe('OrderService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [HttpClientModule],
    providers: [
      {provide: 'BASE_URL', useValue: 'http://localhost'},
    ]
  }));

  it('should be created', () => {
    const service: OrderService = TestBed.get(OrderService);
    expect(service).toBeTruthy();
  });
});
