import { Component } from '@angular/core';
import { WalletService } from '@app/services';

@Component({
  selector: 'navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {
  isExpanded = false;

  constructor(public walletService: WalletService) { }

  collapse(): void {
    this.isExpanded = false;
  }

  toggle(): void {
    this.isExpanded = !this.isExpanded;
  }

  async onConnectWallet(): Promise<void> {
    await this.walletService.connect();
  }

  async onDisconnectWallet(): Promise<void> {
    await this.walletService.disconnect();
  }
}
