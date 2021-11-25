import { Component, OnInit } from '@angular/core';
import { WalletService } from '@app/services';

@Component({
  selector: 'nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent {
  isExpanded = false;

  constructor(public walletService: WalletService) { }

  collapse() : void {
    this.isExpanded = false;
  }

  toggle() : void {
    this.isExpanded = !this.isExpanded;
  }

  async onConnectWallet(): Promise<void> {
    await this.walletService.connect();
  }

  async onDisconnectWallet(): Promise<void> {
    await this.walletService.disconnect();
  }
}
