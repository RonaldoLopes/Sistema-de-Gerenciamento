import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  @Output() toggleSideBarForMe: EventEmitter<any> = new EventEmitter();

  constructor(
    private http: HttpClient,
    private router: Router
    ) { }

  ngOnInit() {
  }

  toggleSideBar(){
    this.toggleSideBarForMe.emit();
  }
  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('access');
    localStorage.removeItem('username');
    localStorage.removeItem('idUser');
    localStorage.removeItem('role');
    this.router.navigate(['/user/login']);
    sessionStorage.clear();
  }

}
