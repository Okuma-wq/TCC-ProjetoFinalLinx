import React from 'react';
import { NavigationContainer } from '@react-navigation/native';
import { createStackNavigator } from '@react-navigation/stack';

import Login from './src/screens/login';
import Main from './src/screens/main';
import Cadastrar from './src/screens/cadastrar';
import Editar from './src/screens/editar'

const AuthStack = createStackNavigator();

export default function Stack(){
  return(
    <NavigationContainer>
      <AuthStack.Navigator
        headerMode = 'none'
      >
        <AuthStack.Screen name = 'Login' component={Login} />
        <AuthStack.Screen name = 'Editar' component={Editar} />
        <AuthStack.Screen name = 'Main' component={Main} />
        <AuthStack.Screen name = 'Cadastrar' component={Cadastrar} />
      </AuthStack.Navigator>
    </NavigationContainer>
  )
}