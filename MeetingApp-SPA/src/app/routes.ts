import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { MemberListResolver } from './_resolvers/member-list.resolver';
import { MemberDetailResolver } from './_resolvers/member-detail.resolver';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberCardComponent } from './members/member-card/member-card.component';
import { AuthGuard } from './_guards/auth.guard';
import { ListsComponent } from './lists/lists.component';
import { MemberListComponent } from './members/member-list/member-list.component';
 import {Routes} from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MessagesComponent } from './messages/messages.component';
import { MemberEditResolver } from './_resolvers/member-edit.resolver';


 export const appRoutes: Routes = [
     {path: '', component: HomeComponent},
     {
         path: '', // Se deja vacio para que solo matchee los path de los childrens
         runGuardsAndResolvers: 'always',
         canActivate: [AuthGuard],
         children: [
            {path: 'members', component: MemberListComponent, resolve: {users: MemberListResolver}},
            {path: 'members/:id', component: MemberDetailComponent, resolve: {user: MemberDetailResolver}},
            {path: 'member/edit', component: MemberEditComponent,
             resolve: {user:  MemberEditResolver}, canDeactivate: [PreventUnsavedChanges] },
            {path: 'messages', component: MessagesComponent},
            {path: 'lists', component: ListsComponent},
         ]
     },
     // si ninguna ruta ingresada coincide, redireccionara a la home, siempre al final tiene que estar
     {path: '**', redirectTo: '', pathMatch: 'full'}
 ];
