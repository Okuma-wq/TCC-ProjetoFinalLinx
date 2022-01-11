import React, { Component } from 'react';
import { KeyboardAvoidingView, Picker, StyleSheet, Text, TextInput, TouchableOpacity, View } from 'react-native';
import AsyncStorage from '@react-native-async-storage/async-storage';

import api from '../services/api';

export default class Login extends Component {
  constructor(props) {
    super(props);
    this.state = {
      nome: '',
      email: '',
      senha: '',
      telefone: '',
      celular: '',
      cnpj: '',
      razaoSocial: '',
      tipoUsuario: 1
    }
  }

  realizarCadastro = async () => {

    try {

      const resposta = await api.post('/Usuario', {
        nome: this.state.nome,
        email: this.state.email,
        senha: this.state.senha,
        telefone: this.state.telefone,
        celular: this.state.celular,
        cnpj: this.state.cnpj,
        razaoSocial: this.state.razaoSocial,
        tipoUsuario: this.state.tipoUsuario

      });


      console.warn('Usuario Cadastrado!')

      this.props.navigation.navigate('Login');
      

      
    } catch (error) {
      console.warn(error)
    }
  };

  render() {
    return (
      <KeyboardAvoidingView style={styles.background}>
          <View style={styles.container}>
            <TextInput
              style={styles.input}
              placeholder="Nome"
              autoCorrect={false}
              onChangeText={nome => this.setState({ nome })}
            />

            <TextInput
              style={styles.input}
              placeholder="Email"
              autoCorrect={false}
              onChangeText={email => this.setState({ email })}
            />

            <TextInput
              style={styles.input}
              placeholder="Senha"
              autoCorrect={false}
              secureTextEntry={true}
              onChangeText={senha => this.setState({ senha })}
            />

            <TextInput
              style={styles.input}
              placeholder="Telefone"
              autoCorrect={false}
              onChangeText={telefone => this.setState({ telefone })}
            />

            <TextInput
              style={styles.input}
              placeholder="Celular"
              autoCorrect={false}
              onChangeText={celular => this.setState({ celular })}
            />

            <TextInput
              style={styles.input}
              placeholder="Cnpj"
              autoCorrect={false}
              onChangeText={cnpj => this.setState({ cnpj })}
            />

            <TextInput
              style={styles.input}
              placeholder="Razão Social"
              autoCorrect={false}
              onChangeText={razaoSocial => this.setState({ razaoSocial })}
            />

            <Picker
              //selectedValue={selectedValue}
              style={{ height: 50, width: 150 }}
              onValueChange={(itemValue, itemIndex) => this.setState({tipoUsuario: itemValue})}
            >
              <Picker.Item label="Lojista" value={0} />
              <Picker.Item label="Fornecedor" value={1} />
            </Picker>

            <TouchableOpacity
              onPress={this.realizarCadastro}
              
              style={styles.btnSubmit}
            >
              <Text style={styles.submitText}>Cadastrar</Text>
            </TouchableOpacity>
          </View>
      </KeyboardAvoidingView>
    )
  }
}

const styles = StyleSheet.create({
  background: {
    flex: 1,
    alignItems: 'center',
    justifyContent: 'center',
    backgroundColor: '#FFB200'
  },

  container: {
    flex: 1,
    alignItems: 'center',
    justifyContent: 'center',
    paddingBottom: 290,
    marginTop: 130
  },

  input: {
    backgroundColor: '#fff',
    width: '90%',
    marginBottom: 20,
    color: 'black',
    fontSize: 17,
    padding: 7,
    borderBottomColor: '#411E5A',
    borderBottomWidth: 2
  },

  btnSubmit: {
    backgroundColor: '#411E5A',
    width: 354,
    height: 60,
    alignItems: 'center',
    justifyContent: 'center',
    borderRadius: 50,
    marginBottom: 15
  },

  submitText: {
    color: '#FFFF',

  },

})
