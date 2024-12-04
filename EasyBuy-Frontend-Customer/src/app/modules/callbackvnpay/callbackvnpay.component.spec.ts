import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CallbackvnpayComponent } from './callbackvnpay.component';

describe('CallbackvnpayComponent', () => {
  let component: CallbackvnpayComponent;
  let fixture: ComponentFixture<CallbackvnpayComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CallbackvnpayComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CallbackvnpayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
