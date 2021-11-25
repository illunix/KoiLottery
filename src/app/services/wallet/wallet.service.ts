import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

import Web3 from 'web3';
import WalletConnectProvider from '@walletconnect/web3-provider';
import { stringify } from 'querystring';


// Web3Modal only works with Angular if using the VanillaJS version
// see https://github.com/Web3Modal/web3modal/issues/190#issuecomment-698318423
// @ts-ignore
const Web3Modal = window.Web3Modal.default;


@Injectable({
  providedIn: 'root',
})
export class WalletService {
  public provider: any;
  public web3: Web3 | undefined;
  public selectedAccount: string | undefined;

  public connected$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

  private web3Modal: any;

  constructor() {
    const providerOptions = {
      walletconnect: {
        package: WalletConnectProvider,
        options: {
          infuraId: '792d1b6084d344b5bf4b5b00815997e2',
        },
      },
    };

    this.web3Modal = new Web3Modal({
      providerOptions,
      network: 'mainnet',
    });
  }

  async fetchNetwork(): Promise<void> {
    this.web3Modal.clearCachedProvider();
    this.web3 = new Web3(this.provider);

    // does not return all accounts, only the selected account (in an array)
    const accounts = await this.web3.eth.getAccounts();
    if (!accounts[0]) throw Error('No account');

    this.selectedAccount = accounts[0];
  }


  async connect(): Promise<void> {
    this.web3Modal.clearCachedProvider();

    this.provider = await this.web3Modal.connect();

    this.provider.on('accountsChanged', () => {
      this.fetchNetwork();
    });

    this.provider.on('chainChanged', () => {
      this.fetchNetwork();
    });

    this.connected$.next(true);

    this.fetchNetwork();
  }

  async disconnect(): Promise<void> {
    if (this.provider.close) {
      this.provider.close();
    }

    this.web3Modal.clearCachedProvider();

    this.web3 = undefined;
    this.provider = undefined;
    this.selectedAccount = undefined;

    this.connected$.next(false);
  }

  getAddress(): string {
    return this.selectedAccount!;
  }

  getAddressShortcut(): string {
    return this.selectedAccount?.substring(0, 2)! + '...' + this.selectedAccount?.substring(38, 42)!;
  }
}
