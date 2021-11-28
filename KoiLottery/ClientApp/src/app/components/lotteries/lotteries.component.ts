import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { WalletService } from '@app/services';

interface Lottery {
  id: string,
  name: string,
  duration: number,
  createdAt: Date,
  participationCostInETH: string,
  participationCostInUSD: string,
  participantsCount: number
  poolInETH: number,
  poolInUSD: number
}

@Component({
  selector: 'lotteries',
  templateUrl: './lotteries.component.html',
  styleUrls: ['./lotteries.component.css']
})
export class LotteriesComponent implements OnInit {
  private baseUrl: string;
  lotteries: Lottery[];

  constructor(
    private http: HttpClient,
    private walletService: WalletService,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this.baseUrl = baseUrl;
  }

  ngOnInit() {
    this.http.get<any>(this.baseUrl + 'api/lotteries').subscribe(data => {
      this.lotteries = data;
    });
  }

  styleLotteryName(i: number): Object {
    if (i == 0) {
      return { color: '#f5a835' };
    } else if (i == 1) {
      return { color: '#fa6d32' };
    } else if (i == 2) {
      return { color: 'red' };
    }
    else {
      return { color: 'black' };
    }
  }

  async onParticipateLottery(lotteryId: string, participationCost: string) {
    if (!this.walletService.connected$.value) {
      await this.walletService.connect();
    }

    if (this.walletService.sendEth(participationCost)) {
      const lotteryParticipantData = {
        lotterId: lotteryId,
        address: this.walletService.getAddress()
      };

      this.http.post<any>(this.baseUrl + 'api/lottery-participants', lotteryParticipantData).subscribe();
    }
  }
}
