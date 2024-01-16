import { useRouter } from 'next/router'

function Charge () {
	const router = useRouter()
	const { chargeId } = router.query
}

export default Charge
