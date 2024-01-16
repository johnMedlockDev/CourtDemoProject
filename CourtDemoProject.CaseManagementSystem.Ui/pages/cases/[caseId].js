import { useRouter } from 'next/router'

function Case () {
	const router = useRouter()
	const { caseId } = router.query
}

export default Case
