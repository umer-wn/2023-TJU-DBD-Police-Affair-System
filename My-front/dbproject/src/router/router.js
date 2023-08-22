import { createRouter, createWebHashHistory } from 'vue-router'



const routes = [
    {
        path: '/',
        component: () => import('../MainPage.vue')
    },
    // {
    //     path: '/login',
    //     component: () => import('../components/log-in.vue')
    // },
    {
        path: '/mainMenu',
        component: () => import('../components/Menu/MainMenu.vue'),
        // redirect: '/mainMenu/Register',
        
        children: [
            {
                path: 'Register',
                component: () => import('../components/PoliceManagement/MyRegister.vue')

            },
            {
                path: 'ChangePermission',
                component: () => import('../components/PoliceManagement/ChangePermission.vue')

            },
            {
                path: 'PoliceStationInfoManagement',
                component: () => import('../components/PoliceManagement/PoliceStationInfoManagement.vue')

            },
        ]
           


    },
    // {
    //     path: '/jwt',
    //     component: () => import('../components/jwt.vue')
    // },
    // {
    //     path: '/404',
    //     component: () => import('../components/My404.vue')
        
    // },

    // {
    //     path: '/menu',
    //     component: () => import('../components/MainMenu.vue'),
    //     redirect: '/menu/ProductionInquiry',  //重定向，访问/a3立即跳转到/a3/student
    //     children: [
    //         {
    //             path: 'ProductionInquiry',
    //             component: () => import('../components/ProductInquiry.vue')

    //         },
    //         {
    //             path: 'liquidation',
    //             component: () => import('../components/liquidation.vue')
    //         },
    //         {
    //             path: 'addproduct',
    //             component: () => import('../components/Add-product.vue')
    //         },
    //         {
    //             path: 'adduser',
    //             component: () => import('../components/AddUser.vue')
    //         },
    //         {
    //             path: 'settlement',
    //             component: () => import('@/components/Business/Settlement/index.vue')
    //         },
    //         {
    //             path: 'transactions',
    //             component: () => import('@/components/Business/Transactions.vue')
    //         },
    //         {
    //             path: 'purchase',
    //             component: () => import('@/components/Business/Purchase.vue')
    //         },
    //         {
    //             path: 'redemption',
    //             component: () => import('@/components/Business/Redemption.vue')
    //         },
    //         {
    //             path: 'viewNav',
    //             component: () => import('../components/ViewNav.vue')
    //         },
    //         {
    //             path: 'searchCustomer',
    //             component: () => import('../components/searchcustomer.vue')
    //         },
    //         {
    //             path: 'bankaccount',
    //             component: () => import('../components/Bank-Account.vue')
    //         }
    //     ]
    // },
    // {
    //     path: '/:pathMatcher(.*)*',
    //     redirect: '/404'
    // }
    
]

const router = createRouter({
    //router格式，hash
    history: createWebHashHistory(),
    routes : routes
    

})

export default router;