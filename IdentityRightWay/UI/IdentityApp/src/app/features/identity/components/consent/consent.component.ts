import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Store } from '@ngrx/store';
import { AppState } from 'src/app/app.state';
import { FetchConsent, FetchAcceptConsent } from '../../store/actions/backend.actions';
import { Observable } from 'rxjs';
import { ConsentStore } from '../../store/reducers/consent.reducer';

@Component({
  selector: 'app-consent',
  templateUrl: './consent.component.html',
  styleUrls: ['./consent.component.scss']
})
export class ConsentComponent implements OnInit {
  returnUrl: any;
  consentData$: Observable<ConsentStore>;

  constructor(private activatedRoute: ActivatedRoute, private store : Store<AppState>) { }

  ngOnInit() {
    this.consentData$ = this.store.select(o => o.identity.consent)

    this.activatedRoute.queryParams.subscribe(o => {
      this.returnUrl = o['returnUrl']
      this.store.dispatch(new FetchConsent({ returnUrl : o['returnUrl']  }))
    })
  }

  accept(){
    this.store.dispatch(new FetchAcceptConsent(
      {
        Button : "yes",
        RememberConsent : true,
        ReturnUrl : this.returnUrl,
        ScopesConsented : [
          "api1",
          "profile",
          "openid"
          ]
      }
    ))
  }

  decline(){

  }

}
