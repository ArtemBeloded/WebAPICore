const App = {
    data() {
        return {
            form: {
                name: '',
                phoneNumber: ''
            },
            contacts: []
        }
    },
    computed: {
        canCreate() {
            return this.form.name.trim() && this.form.phoneNumber.trim()
        }
    },
    methods: {
        async createContact() {
            const { ...contact } = this.form
            const newContact = await request('/api/contacts', 'POST', contact)
            console.log(newContact)
            this.contacts.push(newContact)
            console.log(this.contacts)
            this.form.name = this.form.phoneNumber = ''
        },
        async markContact(id) {
            const contact = this.contacts.find(c => c.id === id)
            const updated = await request(`/api/contacts/${id}`, 'PUT', {
                ...contact,
                marked: true
            })
            contact.marked = updated.marked
        },
        async removeContact(id) {
            await request(`/api/contacts/${id}`, 'DELETE')
            this.contacts = this.contacts.filter(c => c.id !== id)
        }
    },
    async mounted() {
        this.contacts = await request('/api/contacts')
    }
}

async function request(url, method = 'GET', data = null) {
    try {
        const headers = {}
        let body
        if (data) {
            headers['Content-Type'] = 'application/json'
            body = JSON.stringify(data)
        }

        const response = await fetch(url, {
            method,
            headers,
            body
        })
        return await response.json()
    } catch (e) {
        console.warn('Error:', e.message)
    }
}

Vue.createApp(App).mount('#app')

