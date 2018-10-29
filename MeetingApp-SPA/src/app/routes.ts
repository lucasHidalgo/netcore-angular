import { AuthGuard } from './_guards/auth.guard';
import { ListsComponent } from './lists/lists.component';
import { MemberListComponent } from './member-list/member-list.component';
 import {Routes} from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MessagesComponent } from './messages/messages.component';


 export const appRoutes: Routes = [
     {path: '', component: HomeComponent},
     {
         path: '', // Se deja vacio para que solo matchee los path de los childrens 
         runGuardsAndResolvers: 'always',
         canActivate: [AuthGuard],
         children: [
            {path: 'members', component: MemberListComponent},
            {path: 'messages', component: MessagesComponent},
            {path: 'lists', component: ListsComponent},
         ]
     },
     // si ninguna ruta ingresada coincide, redireccionara a la home, siempre al final tiene que estar
     {path: '**', redirectTo: '', pathMatch: 'full'}
 ];
