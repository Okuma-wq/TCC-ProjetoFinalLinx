import React, { Component } from 'react';
import { ImageBackground, KeyboardAvoidingView, Image, StyleSheet, Text, TextInput, TouchableOpacity, View } from 'react-native';
import AsyncStorage from '@react-native-async-storage/async-storage';
import '../../App.css'

import api from '../services/api';

export default class Login extends Component {
  constructor(props) {
    super(props);
    this.state = {
      email: '',
      senha: '',
    }
  }

  cadastrarUsuario = async () => {

    this.props.navigation.navigate('Cadastrar');

  }

  LoginFalse = async () => {

    this.props.navigation.navigate('Main');

  }

  realizarLogin = async () => {
    console.warn(this.state.email + ' ' + this.state.senha);

    try {

      const resposta = await api.post('/Login', {
        email: this.state.email,
        senha: this.state.senha,
      });

      const token = resposta.data;

      console.warn(token);

      await AsyncStorage.setItem('userToken',token);

      this.props.navigation.navigate('Main');

    } catch (error) {
      console.warn(error)
    }
  };

  render() {
    return (
      <ImageBackground
        source={require('../../assets/img/imgfundo.png')}
        style={StyleSheet.absoluteFillObject}
      >
        <View style={styles.overlay} />
        <View style={styles.main} >

          <Image
            style={styles.mainImgLogo}
            source={require('../../assets/img/logo.png')}
          />

          <TextInput
            style={styles.inputLogin}
            placeholder="E-mail"
            placeholderTextColor="#411E5A"
            keyboardType='email-address'
            onChangeText={email => this.setState({ email })}
          />

          <TextInput
            style={styles.inputLogin}
            placeholder="Senha"
            placeholderTextColor="#411E5A"
            secureTextEntry={true}
            onChangeText={senha => this.setState({ senha })}
          />

          <TouchableOpacity
            style={styles.btnLogin}
            onPress={this.realizarLogin}
          >
            <Text style={styles.btnLoginText}>ENTRAR</Text>
          </TouchableOpacity>


          <TouchableOpacity
            style={styles.btnLogin}
            onPress={this.cadastrarUsuario}
          >
            <Text
              style={styles.btnLoginText}>CRIAR CONTA</Text>
          </TouchableOpacity>

          <TouchableOpacity
            style={styles.btnEsqueci}
            onPress={this.LoginFalse}
          >
            <Text style={styles.esqueciText}>Esqueci Minha Senha</Text>
          </TouchableOpacity>

        </View>
      </ImageBackground>
    )
  }
}

const styles = StyleSheet.create({

  overlay: {
    ...StyleSheet.absoluteFillObject,

  },

  // conteúdo da main
  main: {
    width: '100%',
    height: '100%',
    alignItems: 'center',
    justifyContent: 'center',
    
  },

  mainImgLogo: {
    tintColor: '#FFF',
    height: 158,
    width: 282,
    margin:60,
    marginTop: 0
  },

  inputLogin: {
    width: 300,
    marginBottom: 25,
    fontSize: 18,
    color: '#411E5A',
    borderBottomColor: '#411E5A',
    borderBottomWidth: 1
  },
  
  btnLogin: {
    alignItems: 'center',
    justifyContent: 'center',
    height: 60,
    width: 300,
    backgroundColor: '#411E5A',
    borderColor: '#411E5A',
    borderWidth: 1,
    shadowOffset: { height: 1, width: 1 },
    marginTop: 1,
    borderRadius: 50,
    marginBottom: 15
  },

  btnLoginText: {
    fontSize: 12,
    color: '#FFF',
    letterSpacing: 6,
    textTransform: 'uppercase'
  }

});