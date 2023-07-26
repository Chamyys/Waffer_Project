import { createRouter, createWebHistory } from 'vue-router'

import Home from '../components/HelloWorld.vue'
import Table from '../vues/Table.vue'
import NotFound from '../vues/NotFound.vue'
import HelloUser from '../vues/HelloUser.vue'
import EnterVue from '../components/EnterVue.vue'
import Login from '../components/Login.vue'
import Measurer from '../components/Measurer.vue'
import Admin from '../components/Admin.vue'
import ConfigMeasurerers from '../components/ConfigMeasurerers.vue'
import ConfigTechnologists from '../components/ConfigTechnologists.vue'
import AdminChooseConfig from '@/components/AdminChooseConfig.vue'
import AddUser from '@/components/AddUser.vue'
import Delete from '@/components/Delete.vue'
import Edit from '@/components/EditUser.vue'
const scrollBehavior = (to, from, savedPosition) => {
  if (savedPosition) {
    return savedPosition
  } else {
    const position = {}
    if (to.hash) {
      position.selector = to.hash

      if (to.hash === '#anchor2') {
        position.offset = { y: 100 }
      }

      if (to.matched.some((m) => m.meta.scrollToTop)) {
        position.x = 0
        position.y = 0
      }
    }
    return position
  }
}

const routes = [
  /*
  {
    path: '/',
    component: HelloUser,
  },
*/
  {
    path: '/',
    component: EnterVue,
  },
  {
    path: '/Measurer',
    component: Measurer,
  },
  {
    path: '/Admin',
    component: Admin,
    children: [
      {
        path: '/Admin/AddUser',
        name: 'AddUser',
        component: AddUser,
      },
      {
        path: '/Admin/Measurerers',
        name: 'ConfigMeasurerers',
        component: ConfigMeasurerers,
      },
      {
        path: '/Admin/Technologists',
        name: 'ConfigTechnologists',
        component: ConfigTechnologists,
      },
      {
        path: '/Admin/AdminChooseConfig',
        name: 'AdminChooseConfig',
        component: AdminChooseConfig,

        children: [
          {
            path: '/Admin/AdminChooseConfig/Delete',
            name: 'Delete',
            component: Delete,
          },
          {
            path: '/Admin/AdminChooseConfig/Edit',
            name: 'Edit',
            component: Edit,
          },
        ],
      },
    ],
  },

  {
    path: '/Home',
    component: Home,
  },
  {
    path: '/Login/:role',
    name: 'Login',
    component: Login,
  },
  {
    path: '/:catchAll(.*)',
    component: NotFound,
  },
  /*
  {
    path: '/',
    component: HelloUser,
  },
  {
    path: '/table',
    component: Table,
  },
  {
    path: '/:catchAll(.*)',
    component: NotFound,
  },
  */
]

const router = createRouter({
  history: createWebHistory(),
  routes,
  scrollBehavior,
})

export default router
